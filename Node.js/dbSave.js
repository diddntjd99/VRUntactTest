// 1. mongoose 모듈 가져오기
var mongoose = require('mongoose');

//fs 모듈 가져오기
var fs = require('fs');

// 2. testDB 세팅
mongoose.connect('mongodb://localhost:27017/testDB');

// 3. 연결된 testDB 사용
var db = mongoose.connection;

// 4. 연결 실패
db.on('error', function(){
	console.log('Connection Failed!');
});

// 5. 연결 성공
db.once('open', function() {
	console.log('Connected!');
});

// 6. Schema 생성. (혹시 스키마에 대한 개념이 없다면, 입력될 데이터의 타입이 정의된 DB 설계도 라고 생각하면 됩니다.)
var student = mongoose.Schema({
	name : String,
	stu_id : Number,
	face : {
		data: Buffer,
		contentType: String
	}
});

// 7. 정의된 스키마를 객체처럼 사용할 수 있도록 model() 함수로 컴파일
var Student = mongoose.model('Student', student);

// 이미지 가져오기
var buf1 = fs.readFileSync("C:/Users/diddn/OneDrive/바탕 화면/20 공경대/Project/img/ws.jpg", 'base64');
var buf2 = fs.readFileSync("C:/Users/diddn/OneDrive/바탕 화면/20 공경대/Project/img/yw.jpg", 'base64');
var buf3 = fs.readFileSync("C:/Users/diddn/OneDrive/바탕 화면/20 공경대/Project/img/sy.jpg", 'base64');
var buf4 = fs.readFileSync("C:/Users/diddn/OneDrive/바탕 화면/20 공경대/Project/img/jh.jpg", 'base64');
var buf5 = fs.readFileSync("C:/Users/diddn/OneDrive/바탕 화면/20 공경대/Project/img/ar.png", 'base64');

// 8. Student 객체를 new 로 생성해서 값을 입력
var newStudent = new Student({name:'Yang woo sung', stu_id:'1871152', face: {data: buf1, contentType:'image/jpg'}});
var newStudent2 = new Student({name:'Heo ye won', stu_id:'1871292', face: {data: buf2, contentType:'image/jpg'}});
var newStudent3 = new Student({name:'Jeon so yul', stu_id:'1871239', face: {data: buf3, contentType:'image/jpg'}});
var newStudent4 = new Student({name:'Jeon jae hyung', stu_id:'1591029', face: {data: buf4, contentType:'image/jpg'}});
var newStudent5 = new Student({name:'O aram', stu_id:'1871159', face: {data: buf5, contentType:'image/png'}});

// 9. 데이터 저장
newStudent.save(function(error, data){
    if(error){
        console.log(error);
    }else{
        console.log('Saved!')
    }
});
newStudent2.save(function(error, data){
    if(error){
        console.log(error);
    }else{
        console.log('Saved!')
    }
});
newStudent3.save(function(error, data){
    if(error){
        console.log(error);
    }else{
        console.log('Saved!')
    }
});
newStudent4.save(function(error, data){
    if(error){
        console.log(error);
    }else{
        console.log('Saved!')
    }
});
newStudent5.save(function(error, data){
    if(error){
        console.log(error);
    }else{
        console.log('Saved!')
    }
});