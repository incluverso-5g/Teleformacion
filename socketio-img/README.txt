SocketIO-IMG is a simple control application for Miro360, based on socket.io messaging system.

=== 1. Installation & run ===

1. Install nodejs (https://nodejs.org/)
2. Edit configuration file (see below)
3. Run "node img.js"

Server runs at port 3000. You can change the port by editing img.js.


=== 2. Configuration ===

The configuration file is config/config_img.js

There are several examples that you can use to configure it. It basically contains:

- test_name: A text for your web application
- devices: The list of "device ids" that you will control from the application
- sessions: The list of parallel test sessions that you plan to run. Each session contains:
  - name: Identification of the session path
  - runs: The list of active periods in the session. Each one having
    - name: A textual name
    - tag: A unique name of the test case (this will be recorded in the session logs at miro360 tool)
    - device: The "device id" of the device running the session
    - uri: The location of the playlist of the active period of the session
    
    
The configuration must match the configuration that you do at "miro360.ini" file in your HMD.
The file miro360.ini at your HMD device must have the following configuration:

[SocketIO]
uri = ws://localhost:3000/socket.io/?EIO=4&transport=websocket <-- Replace "localhost" from your server IP if running in a different host. Replace 3000 by your port if needed
device = vive_pro <-- It must be EXACTLY the same as the in "sessions->runs->device" AND as in "devices"
enable = 1 <-- It must be 1 (this is the default value)

=== 3. Usage ===
Load the web page "http://localhost:3000" (replace host and port according to your situation, if needed)

Before running the test, fill the field "UserID" of the path.

Launch your miro360 application. You must see updates at your device table (bottom part of your page)

Click "RUN" at the test run that you are needing.


