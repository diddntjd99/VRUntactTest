import face_recognition
import numpy as np
import os
import sys

try:
    #인자값 받기
    android_arg = sys.argv[1]

    db_encoding = []
    db_img_names = []
    #DB 사진 받기
    dbDirname = 'db_save_img'
    files = os.listdir(dbDirname)
    for filename in files:
        name, ext = os.path.splitext(filename)
        if ext == '.jpg' or ext == '.png':
            db_img_names.append(name)
            pathname = os.path.join(dbDirname, filename)
            db_img = face_recognition.load_image_file(pathname)
            face_encoding1 = face_recognition.face_encodings(db_img)[0]
            db_encoding.append(face_encoding1)

    android_encoding = []
    #안드로이드 사진 받기
    androidDirname = 'android_save_img'
    androidName, androidExt = os.path.splitext(android_arg)

    androidPathname = os.path.join(androidDirname, android_arg)
    android_img = face_recognition.load_image_file(androidPathname)
    face_encoding2 = face_recognition.face_encodings(android_img)[0]
    android_encoding.append(face_encoding2)

    # 기존 사진과 새 사진이 일치하는지 확인
    for encoding in android_encoding:
        matches = face_recognition.compare_faces(db_encoding, encoding)
        result = "Unknown"
        
        face_distances = face_recognition.face_distance(db_encoding, encoding)
        best_match_index = np.argmin(face_distances)
        if matches[best_match_index]:
            result = db_img_names[best_match_index]
    
    #일치하면 true, 일치하지 않다면 false
    print(result)
except:
    print("picture")