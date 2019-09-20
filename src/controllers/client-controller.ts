import { getRepository } from 'typeorm'
import { validationResult } from 'express-validator'

import CpfValidator from '../validators/cpf-validator'
import Client from '../entities/client'

class ClientController {
  async findAll (req, res) {
    try {
      const clientRepository = getRepository(Client)
      const clients = await clientRepository.find()
      return res.json(clients)
    } catch (error) {
      console.log('error', error)
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async findByCPF (req, res) {
    const { cpf } = req.params

    try {
      const isValid = CpfValidator.isValid(cpf)

      if (!isValid) {
        return res.status(400).send({ errorMessage: 'CPF is invalid' })
      }

      const clientRepository = getRepository(Client)
      const client = await clientRepository.findOne({ cpf })

      if (!client) {
        return res.status(404).send({ errorMessage: `Client with CPF ${cpf} not found` })
      }

      return res.json(client)
    } catch (error) {
      console.log('error', error)
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async create (req, res) {
    const { body } = req
    const { cpf, birthDate, name } = body

    try {
      const errors = validationResult(req)
      if (!errors.isEmpty()) {
        return res.status(400).json({ errorMessage: errors.array()[0].msg })
      }

      const client = new Client()
      client.cpf = cpf
      client.birthDate = new Date(birthDate)
      client.isActive = true
      client.name = name

      const clientRepository = getRepository(Client)
      const { id } = await clientRepository.save(client)
      return res.status(201).send({ id })
    } catch (error) {
      console.log('error', error)

      if (error.message === 'SQLITE_CONSTRAINT: UNIQUE constraint failed: client.cpf') {
        return res.status(422).send({ errorMessage: 'CPF duplicated on database' })
      }

      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async update (req, res) {
    const { body, params } = req
    const { id } = params

    const { name, birthDate } = body

    try {
      const errors = validationResult(req)
      if (!errors.isEmpty()) {
        return res.status(400).send({ errorMessage: errors.array()[0].msg })
      }

      const clientRepository = getRepository(Client)
      const client = await clientRepository.findOne(id)

      if (!client) {
        return res.status(404).send({ errorMessage: `Client with id ${id} not found` })
      }

      client.name = name
      client.birthDate = new Date(birthDate)

      await clientRepository.save(client)
      return res.status(204).send()
    } catch (error) {
      console.log('error', error)
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async delete (req, res) {
    const { id } = req.params

    try {
      const clientRepository = getRepository(Client)
      const client = await clientRepository.findOne(id)

      if (!client) {
        return res.status(404).send({ errorMessage: `Client with id ${id} not found` })
      }

      client.isActive = false
      await clientRepository.save(client)
      return res.status(204).send()
    } catch (error) {
      console.log('error', error)
      return res.status(500).send({ errorMessage: error.message })
    }
  }
}

export default new ClientController()
