# How to run the app
Go to project root folder and run: `docker-compose up`
Database migrations should be executed automatically

# App usage
1) After all services are up and running in docker-compose, got to localhost:9000 - this port is mapped to nginx container that serves angular app.

    :warning: **nginx is not configured as reverse proxy so direct routing via browser's adress bar is not working yet and may redirect to not found page**

2) Login as one of the following users:
    - Rick Sanchez
    - Morty Smith
    - Jerry Smith
    - Walter White
    - Saul Goodman
    
    Passwords are same for each user: **HelloWorld123**

3) select any chat and start messaging. You can:
    - send both private and group messages
    - reply, edit, and delete messages in **telegram-like manner**

# Used tech stack
Frontend: Angular+TypeScript, Angular Material

Backend: ASP.NET Core 6.0, SQL Server, EntityFramework Core, SignalR, FluentValidation, IdentityServer, Docker, Docker-compose

# WEB API

## endpoints
**messages:**
- <mark style="background-color: #61affe">GET</mark>
    **/messages/{chatRoomId}/{page} |** *get messages in chat room*<br/>
    | Responses:
    - 200 
    - 404 *chatroom not found*
    - 401
    
    <br/>
- <mark style="background-color: #49cc90">POST</mark>
    **/messages/sendPrivate |** *send private message to a user*<br/>
    | Responses:
    - 201
    - 404 *user not found*
    - 400 *invalid input*
    - 401

    <br/>
- <mark style="background-color: #49cc90">POST</mark>
    **/messages |** *send message by chatroom id*<br/>
    | Responses:
    - 201
    - 404 *chatroom not found, replied message not found*
    - 400 *invalid input*
    - 401 

    <br/>
- <mark style="background-color: #50e3c2">PUT</mark>
    **/messages/{messageId} |** *edit message*<br/>
    | Responses:
    - 204
    - 404 *message not found*
    - 400 *invalid input*
    - 401

    <br/>
- <mark style="background-color: #f93e3e">DELETE</mark>
    **/messages/deleteForSelf/{messageId} |** *delete message for user self only*<br/>
    | Responses:
    - 204
    - 404 *message not found* 
    - 401

    <br/>
- <mark style="background-color: #f93e3e">DELETE</mark>
    **/messages/deleteForAll/{messageId} |** *delete message for everyone in a group*<br/>
    | Responses:
    - 204
    - 404 *message not found*
    - 401

    <br/>
**chatrooms:**
- <mark style="background-color: #61affe">GET</mark>
    **/chatrooms |** *get all chatrooms for a user*<br/>
    | Responses:
    - 200 
    - 401

    <br/>
**auth:**
- <mark style="background-color: #49cc90">POST</mark>
    **/auth/login |** *login into account by username and password*<br/>
    | Responses:
    - 200
    - 401 *invalid username or password* 

    <br/>    