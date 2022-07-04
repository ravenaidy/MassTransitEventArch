# stage1 as builder
FROM node:current-alpine3.15 as builder
WORKDIR /masstransit-client-app

# Copy the package.json and install dependencies
COPY ./masstransit-client-app/package*.json ./
RUN npm install

# Build the project
RUN npm run build

# Copy rest of the files
COPY ./masstransit-client-app .

FROM nginx:alpine as production-build
COPY ./masstransit-client-app/.nginx/nginx.conf /etc/nginx/nginx.conf

## Remove default nginx index page
RUN rm -rf /usr/share/nginx/html/*

# Copy from the stage 1
COPY --from=builder /masstransit-client-app/dist /usr/share/nginx/html

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]