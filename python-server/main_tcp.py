import socket
import os
import struct
import json
import torch
import time
import cv2
import datetime
from src import helper, utils_rotate

yolo_LP_detect = torch.hub.load('yolov5', 'custom', path='checkpoints/LP_detector.pt', force_reload=False, source='local')
yolo_license_plate = torch.hub.load('yolov5', 'custom', path='checkpoints/LP_ocr.pt', force_reload=False, source='local')
yolo_license_plate.conf = 0.60

UPLOAD_FOLDER = 'images'
if not os.path.exists(UPLOAD_FOLDER):
    os.makedirs(UPLOAD_FOLDER)

def process_image(image_path):
    img = cv2.imread(image_path)
    start_time = time.time()
    plates = yolo_LP_detect(img, size=640)
    list_plates = plates.pandas().xyxy[0].values.tolist()
    list_read_plates = set()
    lp = "unknown"

    if len(list_plates) == 0:
        lp = helper.read_plate(yolo_license_plate, img)
        if lp != "unknown":
            cv2.putText(img, lp, (7, 70), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (36,255,12), 2)
            list_read_plates.add(lp)
    else:
        for plate in list_plates:
            flag = 0
            x = int(plate[0])
            y = int(plate[1])
            w = int(plate[2] - plate[0])
            h = int(plate[3] - plate[1])
            crop_img = img[y:y+h, x:x+w]
            cv2.rectangle(img, (x, y), (x + w, y + h), (0,0,225), 2)

            for cc in range(2):
                for ct in range(2):
                    lp = helper.read_plate(yolo_license_plate, utils_rotate.deskew(crop_img, cc, ct))
                    if lp != "unknown":
                        list_read_plates.add(lp)
                        cv2.putText(img, lp, (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (36,255,12), 2)
                        flag = 1
                        break
                if flag == 1:
                    break

    end_time = time.time()
    run_time = str(round(end_time - start_time, 2))
    result_text = ', '.join(list_read_plates)
    cv2.imwrite(image_path, img)

    return {
        "result_path": os.path.basename(image_path),
        "plate_text": result_text,
        "run_time": run_time
    }

def start_tcp_server(host='0.0.0.0', port=5001):
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.bind((host, port))
    s.listen(1)
    s.settimeout(1.0)
    print(f"[+] TCP Server listening on {host}:{port}")
    send_notify(b"READY")

    try:
        while True:
            try:
                conn, addr = s.accept()
            except socket.timeout:
                continue
            print(f"[+] Connection from {addr}")
            try:
                # Receive file size
                raw_size = conn.recv(4)
                if not raw_size:
                    continue
                file_size = struct.unpack('!I', raw_size)[0]

                # Receive file data
                data = b''
                while len(data) < file_size:
                    packet = conn.recv(4096)
                    if not packet:
                        break
                    data += packet

                timestamp = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
                image_name = f"{timestamp}.jpg"
                image_path = os.path.join(UPLOAD_FOLDER, image_name)

                with open(image_path, 'wb') as f:
                    f.write(data)

                # Process the image
                result = process_image(image_path)
                response = json.dumps(result).encode('utf-8')

                # Send result length and data
                conn.send(struct.pack('!I', len(response)))
                conn.send(response)
                print(f"[+] Processed and responded to {addr}")

            except Exception as e:
                print(f"[!] Error: {e}")
            finally:
                conn.close()
    except KeyboardInterrupt:
        print("\n[!] Server stopped by user (Ctrl+C)")
        send_notify(b"STOP")
    finally:
        s.close()
        send_notify(b"STOP")

def send_notify(signal):
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    try:
        s.connect(("localhost", 6000))
        s.sendall(signal)
        s.close()
    except:
        pass

if __name__ == '__main__':
    send_notify("Chill")
    start_tcp_server()
