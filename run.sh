echo $1
if [ $1 ]
then
    if [ $1 = 'local' ]
    then
        export EXAM_URL=http://www.mocky.io/v2
        npm run start:dev
    elif [ $1 = 'dev' ]
    then
        npm build
        docker build -t scheduleapi .
        docker stop container_scheduleapi
        docker rm -v container_scheduleapi 
        docker run --name container_scheduleapi -d -e EXAM_URL=http://www.mocky.io/v2 -p 80:80 scheduleapi
    else
        echo 'stage on development'
    fi
else
    echo 'you can pass local or dev on argument'
fi