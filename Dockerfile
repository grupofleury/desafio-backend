FROM node:alpine

RUN mkdir -p /var/www/api
WORKDIR /var/www/api

COPY package.json ./
RUN npm install
COPY . ./

EXPOSE 4446

CMD ["npm", "run", "dev"]