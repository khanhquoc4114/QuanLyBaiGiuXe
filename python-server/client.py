import socket
import struct
import json

def send_image_to_server(image_path, host='127.0.0.1', port=54321):
    with open(image_path, 'rb') as f:
        image_data = f.read()

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((host, port))
        
        # Gửi prefix
        s.sendall(b"IMG ")
        
        # Gửi độ dài ảnh (4 bytes - unsigned int, big endian)
        s.sendall(struct.pack('!I', len(image_data)))
        
        # Gửi nội dung ảnh
        s.sendall(image_data)
        
        # Nhận độ dài phản hồi
        raw_size = s.recv(4)
        if len(raw_size) < 4:
            raise Exception("Không nhận được kích thước phản hồi")
        response_size = struct.unpack('!I', raw_size)[0]
        
        # Nhận phản hồi JSON
        data = b''
        while len(data) < response_size:
            packet = s.recv(4096)
            if not packet:
                break
            data += packet
        
        response = json.loads(data.decode('utf-8'))
        return response

if __name__ == '__main__':
    image_path = 'images/car.jpg'
    result = send_image_to_server(image_path)
    print("Kết quả nhận diện:")
    print(f"Biển số: {result['plate_text']}")
    print(f"Thời gian xử lý: {result['run_time']}s")