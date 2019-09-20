class CPFValidator {
  BLACKLIST = [
    '00000000000',
    '11111111111',
    '22222222222',
    '33333333333',
    '44444444444',
    '55555555555',
    '66666666666',
    '77777777777',
    '88888888888',
    '99999999999',
    '12345678909'
  ]

  LOOSE_STRIP_REGEX = /[^\d]/g

  verifierDigit (numbers) {
    const numbersAux = numbers.split('').map(number => parseInt(number, 10))
    const modulus = numbersAux.length + 1
    const multiplied = numbersAux.map((number, index) => number * (modulus - index))
    const mod = multiplied.reduce((buffer, number) => buffer + number) % 11
    return (mod < 2 ? 0 : 11 - mod)
  }

  strip (number) {
    return (number || '').toString().replace(this.LOOSE_STRIP_REGEX, '')
  }

  isValid (number) {
    const stripped = this.strip(number)

    // CPF must be defined
    if (!stripped) {
      return false
    }

    // CPF must have 11 chars
    if (stripped.length !== 11) {
      return false
    }

    // CPF can't be blacklisted
    if (this.BLACKLIST.indexOf(stripped) >= 0) {
      return false
    }

    let numbers = stripped.substr(0, 9)
    numbers += this.verifierDigit(numbers)
    numbers += this.verifierDigit(numbers)

    return numbers.substr(-2) === stripped.substr(-2)
  }
}

export default new CPFValidator()
