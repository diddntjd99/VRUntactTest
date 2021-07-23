var Client = require('mongodb').MongoClient;
var fs = require('fs');

Client.connect('mongodb://localhost:27017/', function(error, db){
	if(error) {
		console.log(error);
	} else {
		var db_name = db.db("testDB");
        
		var cursor = db_name.collection("students").find();
		cursor.each(function(err,doc){ // document 가 예약어이기 때문에 변수명을 doc로 변경
			if(err){
				console.log(err);
			}else{
				if(doc != null){
					var buf = new Buffer(doc.face.data + doc.face.contentType, 'base64');
					fs.writeFile("db_save_img/" + doc.stu_id +".jpg", buf, function(err) {
						if(err)
							console.log(err);
						else
							console.log("Save DB Iamge");
					});
				}
			}
		});
		db.close();
	}
});