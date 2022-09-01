FROM node:latest as build
WORKDIR /usr/src/app
COPY FrontEnd/chat-app/package.json FrontEnd/chat-app/package-lock.json ./
RUN npm install
COPY FrontEnd/chat-app/. .
RUN npm run build

FROM nginx:latest
COPY --from=build usr/src/app/dist/chat-app/ /usr/share/nginx/html/
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]