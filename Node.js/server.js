const express = require('express');
const app = express();
var socketio = require('socket.io');
var fs = require('fs');
const { PythonShell } = require('python-shell');

var port = 3235;
var server = app.listen(port, () => {
	console.log('Example app listening on port ' + port + '!');
});

var io = socketio.listen(server);

const { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } = require('constants');
var app_http = express();
var server = app_http.listen(9000, function(){
    console.log("Express server has started on port 9000")
})

var io_unity = require('socket.io')(process.env.PORT || 52300)
var result;
var studentsAnswer = [];
var page;
var student_id

console.log('Server has started');

io.on('connection', function(socket){
	console.log("Login New Client");
	
	var id;
	var androidPath;
	
	socket.on('send_id', function(data) {
		console.log("stu_id : " + data);
		id = data;
		student_id = id;
		
		androidPath = "android_" + id + ".jpg";
	});
	
	socket.on('send_img', function(data) {
		var buf = new Buffer(data + 'image/jpg', 'base64');
		fs.writeFile("android_save_img/" + androidPath, buf, function(err) {
			if(err)
				console.log(err);
			else
				console.log("Save Android Iamge");
		});
		
		var options = {
			mode: 'text',
			pythonPath: "C:\\Users\\diddn\\anaconda3\\envs\\fr\\python.exe",
			pythonOptions: ['-u'],
			scriptPath: '',
			args: [androidPath]
		};
		
		PythonShell.run('FR.py', options, function(err, result) {
			if(err) throw err;
			console.log(result);
			
			if (result == id) {
				socket.emit("reply", 'true'); //Node.js에서 안드로이드로 전송
			} else if(result == "picture") {
				socket.emit("reply", 'picture'); //Node.js에서 안드로이드로 전송
			} else {
				socket.emit("reply", 'false'); //Node.js에서 안드로이드로 전송
			}
		});
	});
});


// mongoDB 연동
// 1. mongoose 모듈 가져오기
var mongoose = require('mongoose');
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
    studentID : 'string',
    question1 : 'string',
    question2 : 'string',
    question3 : 'string',
    question4 : 'string',
    question5 : 'string',
    question6 : 'string',
    question7 : 'string',
    question8 : 'string',
    question9 : 'string',
    question10 : 'string'
});

// 7. 정의된 스키마를 객체처럼 사용할 수 있도록 model() 함수로 컴파일
var Student = mongoose.model('Schema', student);

// 유니티 nodejs 통신
io_unity.on('connection', function(socket) {
    console.log('Connection Made!');

    var accessCount = 0;
    var answer = [];
    
    socket.on('msg', function(data){
        if(accessCount == 0){
            var obj_keys = Object.keys(data); // => obj_keys는 dictionary의 keys 와 values
            //console.log(data[keys[1]]);
            var values_indexes = Object.keys(data[obj_keys[1]]);

            //console.log(Object.keys(data[obj_keys[1]]));
            for(var i = 0; i < values_indexes.length; i++) {
                answer[answer.length] = data[obj_keys[1]][values_indexes[i]];
            }

            // 8. Student 객체를 new 로 생성해서 값을 입력
            var newStudent = new Student({studentID:student_id, question1:answer[1], question2:answer[2],
                question3:answer[3], question4:answer[4], question5:answer[5], question6:answer[6],
                question7:answer[7], question8:answer[8], question9:answer[9], question10:answer[10]});

            // 9. 데이터 저장
            newStudent.save(function(error, data){
                if(error){
                    console.log(error);
                }else{
                    console.log('Saved!')
                }
            });

            // 10. Student 레퍼런스 전체 데이터 가져오기
            Student.find(function(error, students){
                console.log('--- Read all ---');
                if(error){
                    console.log(error);
                }else{
                    //console.log(students);
                }
            })
            result = data;
            studentsAnswer[studentsAnswer.length] = answer;
            accessCount += 1;
        }
    });

    socket.on('disconnect', function() {
        

        console.log('A player has disconnected');
    });
});

app_http.get('/', function(req, res){
    var str = "";
    str = str + '<style>table {width: 100%;border: 1px solid #444444;border-collapse: collapse;table-layout: fixed;}th, td {border: 1px solid #444444;}</style>';
    str = str + '<table><tr>';
    str = str + "<th>학번</th>";
    str = str + "<th>문제1</th>";
    str = str + "<th>문제2</th>";
    str = str + "<th>문제3</th>";
    str = str + "<th>문제4</th>";
    str = str + "<th>문제5</th>";
    str = str + "<th>문제6</th>";
    str = str + "<th>문제7</th>";
    str = str + "<th>문제8</th>";
    str = str + "<th>문제9</th>";
    str = str + "<th>문제10</th></tr>";
    

    // 학생 답안 데이터
    for(var i = 0; i< studentsAnswer.length; i++){
        var strAns = JSON.stringify(studentsAnswer[i]).replace(/[\[\]']+/g,'') // 대괄호 "[" , "]" 지우기
        var strSplit = strAns.split(',');
        str = str + '<tr>';
        //console.log(strSplit);
        for(var j = 0; j< strSplit.length; j++){
            //console.log(strSplit[j].length);
            if(j == 0){
                str = str + '<td>' + student_id + '</td>'; 
            }
            else{
                str = str + '<td>' + strSplit[j] + '</td>';
            }
        }
        str = str + '</tr>';
    }
    str = str + '</table>';
    res.send(str);
    //res.send(students);
    
});