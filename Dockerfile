FROM mhart/alpine-node:10

RUN mkdir -p /usr/src/scheduleapi 
WORKDIR /usr/src/scheduleapi
ADD package.json /usr/src/scheduleapi

RUN npm i --production

ADD ormconfig.json /usr/src/scheduleapi
ADD build /usr/src/scheduleapi/

RUN npm run migration:generate Client
RUN npm run migration:run

EXPOSE 80

CMD [ "npm","start"]