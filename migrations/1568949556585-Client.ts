import {MigrationInterface, QueryRunner} from "typeorm";

export class Client1568949556585 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`CREATE TABLE "exam" ("id" integer PRIMARY KEY AUTOINCREMENT NOT NULL, "name" varchar NOT NULL, "value" integer NOT NULL, "externalId" varchar NOT NULL, "createdAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "updatedAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP))`, undefined);
        await queryRunner.query(`CREATE TABLE "schedule" ("id" integer PRIMARY KEY AUTOINCREMENT NOT NULL, "initialDate" datetime NOT NULL, "finalDate" datetime NOT NULL, "isActive" boolean NOT NULL DEFAULT (1), "createdAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "updatedAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "clientId" integer, "examId" integer)`, undefined);
        await queryRunner.query(`CREATE TABLE "client" ("id" integer PRIMARY KEY AUTOINCREMENT NOT NULL, "cpf" varchar NOT NULL, "birthDate" datetime NOT NULL, "name" varchar NOT NULL, "isActive" boolean NOT NULL DEFAULT (1), "createdAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "updatedAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), CONSTRAINT "UQ_9921dca81551c93e5a459ef03ce" UNIQUE ("cpf"))`, undefined);
        await queryRunner.query(`CREATE TABLE "temporary_schedule" ("id" integer PRIMARY KEY AUTOINCREMENT NOT NULL, "initialDate" datetime NOT NULL, "finalDate" datetime NOT NULL, "isActive" boolean NOT NULL DEFAULT (1), "createdAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "updatedAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "clientId" integer, "examId" integer, CONSTRAINT "FK_3d81c1bcc9ffe860e54edf97dcb" FOREIGN KEY ("clientId") REFERENCES "client" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION, CONSTRAINT "FK_d123c124713e9cc95eb022e75f5" FOREIGN KEY ("examId") REFERENCES "exam" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION)`, undefined);
        await queryRunner.query(`INSERT INTO "temporary_schedule"("id", "initialDate", "finalDate", "isActive", "createdAt", "updatedAt", "clientId", "examId") SELECT "id", "initialDate", "finalDate", "isActive", "createdAt", "updatedAt", "clientId", "examId" FROM "schedule"`, undefined);
        await queryRunner.query(`DROP TABLE "schedule"`, undefined);
        await queryRunner.query(`ALTER TABLE "temporary_schedule" RENAME TO "schedule"`, undefined);
    }

    public async down(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`ALTER TABLE "schedule" RENAME TO "temporary_schedule"`, undefined);
        await queryRunner.query(`CREATE TABLE "schedule" ("id" integer PRIMARY KEY AUTOINCREMENT NOT NULL, "initialDate" datetime NOT NULL, "finalDate" datetime NOT NULL, "isActive" boolean NOT NULL DEFAULT (1), "createdAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "updatedAt" datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), "clientId" integer, "examId" integer)`, undefined);
        await queryRunner.query(`INSERT INTO "schedule"("id", "initialDate", "finalDate", "isActive", "createdAt", "updatedAt", "clientId", "examId") SELECT "id", "initialDate", "finalDate", "isActive", "createdAt", "updatedAt", "clientId", "examId" FROM "temporary_schedule"`, undefined);
        await queryRunner.query(`DROP TABLE "temporary_schedule"`, undefined);
        await queryRunner.query(`DROP TABLE "client"`, undefined);
        await queryRunner.query(`DROP TABLE "schedule"`, undefined);
        await queryRunner.query(`DROP TABLE "exam"`, undefined);
    }

}
