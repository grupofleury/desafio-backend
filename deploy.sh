echo $1
if [ $1 ]
then
    if [ $1 = 'local' ]
    then
        export EXAM_URL=http://www.mocky.io/v2
        export DATABASE_NAME=dataset.db

        npm run migration:local:run
        npm run start:local
    elif [ $1 = 'dev' ]
    then
        export DATABASE_NAME='dataset.db'
        export EXAM_URL='http://www.mocky.io/v2'

        npm run migration:local:generate Client
        
        npm run build
        docker build --build-arg database_name=$DATABASE_NAME -t scheduleapi .
        docker stop container_scheduleapi
        docker rm -v container_scheduleapi

        docker run --name container_scheduleapi -d -e DATABASE_NAME=$DATABASE_NAME -e EXAM_URL=$EXAM_URL -p 80:80 scheduleapi
    elif [ $1 = 'staging' ]
    then
        DATABASE_NAME=$DATABASE_NAME
        npm run build
        docker build --build-arg database_name=$DATABASE_NAME -t scheduleapi .
        echo 'push your image'
    else
        echo 'stage on development'
    fi
else
    echo 'you can pass local or dev on argument'
fi