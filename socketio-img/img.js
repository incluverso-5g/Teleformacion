var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);

app.get('/', function(req, res){
  res.sendFile(__dirname + '/static/img.html');
});

app.use('/conf', express.static(__dirname + '/conf'));
app.use('/playlists', express.static(__dirname + '/playlists'));
app.use('/', express.static(__dirname + '/static'))

function log(textline) {
  d = new Date();
  console.log(d.toISOString() + " " + textline);
}

io.on('connection', function(socket){
  log('user connected');
  socket.broadcast.emit('log', 'user connected');

  socket.on('disconnect', function(){
     log('user disconnected');
     socket.broadcast.emit('log', 'user disconnected');
  });
  
  socket.on('join', function(msg) {
    socket.join(msg.group);
    log('user joined ' + msg.group);
    socket.broadcast.emit('log', 'user joined ' + msg.group);
  });
  socket.on('leave', function(msg) {
    socket.leave(msg.group);
    log('user left ' + msg.group)
    socket.broadcast.emit('log', 'user left ' + msg.group);
  });

  socket.on('dr_command', function(msg){
    log('DR Command['+ msg.group + '][' + msg.tag + '][' + msg.user_id + ']');
    io.to(msg.group).emit('dr_command', msg);
  });
  socket.on('dr_status', function(msg){
    log('DR Status['+ msg.group + '][' + msg.tag + '][' + msg.user_id + ']: ' + msg.message);
    io.emit('dr_status', msg);
  });

});

http.listen(3000, function(){
  log('listening on *:3000');
});
