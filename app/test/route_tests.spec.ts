import { expect } from 'chai';
import { Tests } from '../tests';


describe('Chamadas api', () => {
  it('testa lista exame', async () => {
    let teste = new Tests();
    expect(await teste.testaListaExame()).ok;
  });

  it('testaNovoCliente', async() => {
    let teste = new Tests();
    expect(await teste.testaNovoCliente());
  });

  it('testaUpdateCliente', async() => {
    let teste = new Tests();
    expect(await teste.testaUpdateCliente()).ok;
  });

  it('deletaCliente', async() => {
    let teste = new Tests();
    expect(await teste.deletaCliente()).ok;
  });

  it('findCliente', async() => {
    let teste = new Tests();
    expect(await teste.findCliente()).ok;
  });

  it('listaCliente', async() => {
    let teste = new Tests();
    expect(await teste.listaCliente()).ok;
  });

  it('findAgendamentos', async() => {
    let teste = new Tests();
    expect(await teste.findAgendamentos()).ok;
  });

  it('deleteAgendamento', async() => {
    let teste = new Tests();
    expect(await teste.deleteAgendamento()).ok;
  });

  it('updateAgendamento', async() => {
    let teste = new Tests();
    expect(await teste.updateAgendamento()).ok;
  });

  it('agendamentoCliente', async() => {
    let teste = new Tests();
    expect(await teste.agendamentoCliente()).ok;
  });
});