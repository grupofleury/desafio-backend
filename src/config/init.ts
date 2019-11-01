import { resolve } from 'path'
import { config } from 'dotenv'

config({ path: resolve(__dirname, '../../env/' + (process.env.NODE_ENV || 'local') + '.env') })