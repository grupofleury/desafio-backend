import Faker from 'faker'

const getCpf = () => Faker.random.number({ 'min': 100000000,'max': 999999999 }) + '-' + Faker.random.number({ 'min': 10, 'max': 99 })

const fullName = () => Faker.name.firstName + ' ' + Faker.name.lastName

const getDate = () => Faker.date.between('1920-01-01', '2019-09-01')

const getFutureDate = () => Faker.date.between(new Date(), '2020-12-31')

const getExamsMock = () => '{"exams": [{"id":"1","name":"17 soro","value":35.60},{"id":"2","name":"Acidificação Urinária","value":84.90},{"id":"3","name":"Ácido Ascórbico, plasma","value":99.90}]}'

export {
    getCpf,
    fullName,
    getDate,
    getFutureDate,
    getExamsMock
}