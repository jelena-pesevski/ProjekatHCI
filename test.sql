insert into usluga (Naziv, Cijena) values ('Popravka masine za ves', 200.00);
insert into usluga (Naziv, Cijena) values ('Popravka masine za sudje', 190.00);
insert into usluga (Naziv, Cijena) values ('Popravka frizidera', 250.00);
insert into usluga (Naziv, Cijena) values ('Zamjena centrifuge', 200.00);
insert into usluga (Naziv, Cijena) values ('Zamjena hladnjaka', 80.50);

insert into rezervnidio (Naziv, Cijena, Kolicina) values ( 'Remer za ves masinu', 10.00, 20);
insert into rezervnidio (Naziv, Cijena, Kolicina) values ( 'Motor za ves masinu Gorenje', 50.00, 5);
insert into rezervnidio (Naziv, Cijena, Kolicina) values ( 'Kompresor za frizider R134a', 104.00, 7);
insert into rezervnidio (Naziv, Cijena, Kolicina) values ( 'Kondenzator', 84.00, 6);
insert into rezervnidio (Naziv, Cijena, Kolicina) values ('Bubanj za ves masinu', 150.00, 8);

update prijavakvara set Status='u toku' where IdPrijave=1;
select * from popravka_rezervnidio;
select * from rezervnidio;
select * from majstor;
select * from račun;
select * from račun_stavka;
UPDATE rezervnidio SET Naziv='Hladnjaca', Cijena=120.00, Kolicina=3 WHERE Sifra=6;
