select * from TesteExtra;
select * from postagem where Id = 154;

select * from Usuario;
select * from postagem where UsuarioId = 2;
select * from curtidas;
SELECT COUNT(*) AS Contagem FROM TesteExtra WHERE IdSolicitado = 25 AND IdSolicitante = 8 AND Status = 1;

insert into TesteExtra values (8, 28, 1);

select * from curtidas where UsuarioId = 8;
delete from curtidas where UsuarioId = 8;

select * from TesteExtra where IdSolicitado = 8;

DELETE FROM TesteExtra WHERE IdSolicitado = 28 AND IdSolicitante = 8 AND Status = 1;