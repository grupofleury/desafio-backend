FROM mhart/alpine-node:10

RUN mkdir -p /usr/src/scheduleapi 
WORKDIR /usr/src/scheduleapi
ADD package.json /usr/src/scheduleapi

RUN npm i --production

ARG database_name=default_value
ENV DATABASE_NAME=$database_name

ADD ormconfig.js /usr/src/scheduleapi
ADD build /usr/src/scheduleapi/

RUN npm run migration:run

EXPOSE 80

CMD [ "npm","start"]