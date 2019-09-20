import {Entity,Column,PrimaryGeneratedColumn} from "typeorm";

@Entity('cliente')
export class Cliente {
    
    @PrimaryGeneratedColumn()
    id: number;

    @Column({nullable:true})
    nome: string;

    @Column({nullable:true})
    idade: number

    @Column({nullable:true})
    email: string;

    @Column({nullable:false})
    cpf: string;

    @Column({nullable:true})
    genero: boolean;

    @Column({nullable:true})
    endereco: string;

    @Column({nullable:true})
    cidade: string;
}