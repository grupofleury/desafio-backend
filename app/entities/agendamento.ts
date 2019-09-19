import {Entity,Column,PrimaryGeneratedColumn} from "typeorm";
import {MinLength,MaxLength} from "class-validator";

@Entity('agendamento')
export class Agendamento {
    @PrimaryGeneratedColumn()
    id: number;
    
    
    @Column({nullable:false})
    @MinLength(10, {
        message: "cpf is too short"
    })
    @MaxLength(12, {
        message: "cpf is too long"
    })
    cpf: string;

    @Column({nullable:true})
    name: string;

    @Column({nullable:true,type:'double'})
    value: number;

    @Column({nullable:true, type:"date"})
    data: string;

    @Column({nullable:true})
    horario: string;

    @Column({nullable:false})
    idAgendamento: string;
}