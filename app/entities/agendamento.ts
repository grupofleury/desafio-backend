import {Entity,Column,PrimaryGeneratedColumn} from "typeorm";

@Entity('agendamento')
export class Agendamento {
    @PrimaryGeneratedColumn()
    id: number;
    
    @Column({nullable:true})
    cpf_cliente: string;

    @Column({nullable:true})
    exame_nome: string;
    
    @Column({nullable:true})
    exame_id: number;

    @Column({nullable:true})
    exame_valor: number;

    @Column({nullable:true})
    data: number;

    @Column({nullable:true})
    hora: number;
    
}