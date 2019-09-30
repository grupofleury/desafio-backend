FROM node:alpine

RUN mkdir -p /var/www/api
WORKDIR /var/www/api

COPY package.json ./
RUN npm install
COPY . ./

EXPOSE 3000

CMD ["npm", "run", "dev"]