import socket
import os
import struct
import json
import torch
import time
import cv2
from src import helper, utils_rotate
import numpy as np

plate_detect = torch.hub.load('yolov5', 'custom', path='models/plate_det.pt', force_reload=False, source='local')
char_detect = torch.hub.load('yolov5', 'custom', path='models/char_det.pt', force_reload=False, source='local')
char_detect.conf = 0.60

def process_image_bytes(data_bytes):    
    img_array = np.frombuffer(data_bytes, np.uint8)
    img = cv2.imdecode(img_array, cv2.IMREAD_COLOR)
    start_time = time.time()
    plates = plate_detect(img, size=640)
    list_plates = plates.pandas().xyxy[0].values.tolist()
    list_read_plates = set()
    lp = "unknown"

    if len(list_plates) == 0:
        lp = helper.read_plate(char_detect, img)
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
            cv2.rectangle(img, (x, y), (x + w, y + h), (0, 0, 225), 2)

            for cc in range(2):
                for ct in range(2):
                    lp = helper.read_plate(char_detect, utils_rotate.deskew(crop_img, cc, ct))
                    if lp != "unknown":
                        list_read_plates.add(lp)
                        cv2.putText(img, lp, (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (36, 255, 12), 2)
                        flag = 1
                        break
                if flag == 1:
                    break

    end_time = time.time()
    run_time = str(round(end_time - start_time, 2))
    result_text = ', '.join(list_read_plates)

    return {
        "plate_text": result_text,
        "run_time": run_time
    }

def start_tcp_server(host='0.0.0.0', port=54321):
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind((host, port))
    s.listen(1)
    print(f"[+] TCP Server listening on {host}:{port}")

    try:
        while True:
            conn, addr = s.accept()
            print(f"[+] Connection from {addr}")
            try:
                prefix = conn.recv(4).decode()
                print(f"[>] Prefix: {prefix}")

                if prefix == "CMD ":
                    cmd = conn.recv(1024).decode().strip()
                    print(f"[CMD] {cmd}")
                    if cmd == "ping":
                        conn.sendall(b"pong\n")
                    elif cmd == "status":
                        conn.sendall(b"model loaded\n")
                    else:
                        conn.sendall(b"unknown command\n")
                    conn.close()
                    continue

                elif prefix == "IMG ":
                    raw_size = conn.recv(4)
                    file_size = struct.unpack('!I', raw_size)[0]

                    data = b''
                    while len(data) < file_size:
                        packet = conn.recv(4096)
                        if not packet:
                            break
                        data += packet

                    result = process_image_bytes(data)
                    response = json.dumps(result).encode('utf-8')

                    conn.send(struct.pack('!I', len(response)))
                    conn.send(response)
                    print(f"[IMG] Processed and responded to {addr}")
                    conn.close()

                else:
                    print(f"[!] Unknown prefix: {prefix}")
                    conn.sendall(b"invalid prefix\n")
                    conn.close()

            except Exception as e:
                print(f"[!] Error: {e}")
                conn.close()
    except KeyboardInterrupt:
        print("\n[!] Server stopped.")
    finally:
        s.close()

if __name__ == '__main__':
    start_tcp_server()