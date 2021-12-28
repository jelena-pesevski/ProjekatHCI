-- trigeri
ALTER TABLE zaposleni MODIFY Tema INTEGER;
ALTER TABLE zaposleni ALTER Tema SET DEFAULT 1;


delimiter $$
create trigger postavi_zaposlenog after insert on zaposleni
for each row
begin
if (new.Tip= 'O') then
 insert into operater (IdZaposlenog) values (new.IdZaposlenog);
elseif (new.Tip='M') then
 insert into majstor (IdZaposlenog) values (new.IdZaposlenog);
elseif (new.Tip='A') then
 insert into administrator (IdZaposlenog) values (new.IdZaposlenog);
 end if;
end$$
delimiter ;

delimiter $$
create trigger umanji_kolicinu_rezervnog_dijela after insert on popravka_rezervnidio
for each row
begin
	update rezervnidio r set r.Kolicina=r.Kolicina-new.Kolicina where r.Sifra=new.Sifra;
end$$
delimiter ;

delimiter $$
create trigger uvecaj_cijenu_racuna after insert on račun_stavka
for each row
begin
	update račun r set r.Cijena=(r.Cijena+(new.Cijena*new.Kolicina)) where r.IdRacuna=new.IdRacuna;
end$$
delimiter ;
	
delimiter $$
create trigger umanji_zaduzenja after update on prijavakvara
for each row
begin
	if(new.Status='zavrseno') then
		update majstor 
        set BrojZaduzenja=BrojZaduzenja-1
        where IdZaposlenog=new.Majstor_IdZaposlenog;
    end if;
end$$
delimiter ;

delimiter $$
create procedure dodijeli_zaduzenje_majstoru(in pIdPrijave int(11))
begin
	set @majstor_id=(select m.IdZaposlenog from majstor m inner join zaposleni z on m.IdZaposlenog=z.IdZaposlenog 
    where (BrojZaduzenja=(select min(BrojZaduzenja) from majstor) and Status='aktivan') limit 1);
    update prijavakvara set Majstor_IdZaposlenog=@majstor_id where IdPrijave=pIdPrijave;
    update majstor set BrojZaduzenja=BrojZaduzenja+1 where IdZaposlenog=@majstor_id;
end$$
delimiter ;

delimiter $$
create procedure zavrsi_popravku(in pIdPopravke int(11))
begin
	update popravka set Zavrsetak=now(), Zavrseno=1 where IdPopravke=pIdPopravke;
    set @prijavaId=(select IdPrijave from popravka where IdPopravke=pIdPopravke);
    update prijavakvara set Status='zavrseno' where IdPrijave=@prijavaId;
end$$
delimiter ;

create view individualni_klijent_info as
select i.IdKlijenta, Ime, Prezime, Adresa, BrojTelefona
from individualni i
inner join klijent k on i.IdKlijenta=k.IdKlijenta
inner join telefonklijenta t on i.IdKlijenta=t.IdKlijenta;

create view preduzece_klijent_info as
select p.IdKlijenta, Naziv, Adresa, BrojTelefona
from preduzeće p
inner join klijent k on p.IdKlijenta=k.IdKlijenta
inner join telefonklijenta t on p.IdKlijenta=t.IdKlijenta;

create view zavrsene_popravke as
select *
from popravka
where Zavrsetak!=null;

create view majstori_detaljno as
select m.IdZaposlenog, Ime, Prezime, Plata, Status, BrojZaduzenja
from majstor m
inner join zaposleni z on z.IdZaposlenog=m.idZaposlenog;

create view nezavrsene_popravke_sa_opisom as
select IdPopravke, IdZaposlenog, IdPrijave, Pocetak, Opis 
from popravka 
natural join prijavakvara
where Zavrseno=0;

delimiter $$zaposleniKorisničkoIme
-- procedura koja unosi usluge u racun 
create procedure unesi_usluge_u_racun(in pIdRacuna int(11), in pIdPopravke int(11))
begin
	declare vIdUsluge int(11);
    declare vCijena decimal(6,2);
    declare vKolicina int;
    declare vKraj bool default false;
	declare cUsluge cursor for 
    select IdUsluge,Kolicina,Cijena from popravka_usluga
    where IdPopravke=pIdPopravke;
    declare continue handler for not found set vKraj=true;
	
    open cUsluge;
    
    petlja: loop
		fetch cUsluge into vIdUsluge, vKolicina, vCijena;
        
        if vKraj then
			leave petlja;
		end if;
        
        insert into račun_stavka (IdRacuna, Cijena, Kolicina, IdUsluge, RezervniDio_Sifra) values (pIdRacuna,vCijena, vKolicina, vIdUsluge, null);
    
    end loop petlja;
	close cUsluge;
end$$
delimiter ;

delimiter $$
-- procedura koja unosi rezervne dijelove u racun
create procedure unesi_rezDijelove_u_racun(in pIdRacuna int(11), in pIdPopravke int(11))
begin
	declare vSifra int(11);
    declare vCijena decimal(6,2);
    declare vKolicina int;
    declare vKraj bool default false;
	declare cDijelovi cursor for 
    select Sifra,Kolicina,Cijena from popravka_rezervnidio
    where IdPopravke=pIdPopravke;
	declare continue handler for not found set vKraj=true;
	
    open cDijelovi;
    
    petlja: loop
		fetch cDijelovi into vSifra, vKolicina, vCijena;
        
        if vKraj then
			leave petlja;
		end if;
        
        insert into račun_stavka (IdRacuna, Cijena, Kolicina, IdUsluge, RezervniDio_Sifra) values (pIdRacuna,vCijena, vKolicina, null, vSifra);

    end loop petlja;
	close cDijelovi;
end$$
delimiter ;

create view stavke_usluge as
select u.Naziv, s.Kolicina, s.Cijena, s.Kolicina*s.Cijena as Ukupno, s.IdRacuna
from račun_stavka s 
inner join usluga u on s.IdUsluge=u.IdUsluge;

create view stavke_dijelovi as
select d.Naziv, s.Kolicina, s.Cijena, s.Kolicina*s.Cijena as Ukupno, s.IdRacuna
from račun_stavka s
inner join rezervnidio d on s.RezervniDio_Sifra=d.Sifra;
