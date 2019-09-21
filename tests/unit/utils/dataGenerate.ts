import Faker from 'faker'

const getCpf = () => Faker.random.number({ 'min': 100000000,'max': 999999999 }) + '-' + Faker.random.number({ 'min': 10, 'max': 99 })

const fullName = () => Faker.name.firstName + ' ' + Faker.name.lastName

const getDate = () => Faker.date.between('1920-01-01', '2019-09-01')

export {
    getCpf,
    fullName,
    getDate
}