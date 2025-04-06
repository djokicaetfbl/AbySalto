Aplikacija je nadograđena na postojeći projekat AbySalto (projkat je kloniran i dalje je nastavljen razvoj lokalno).
Razvoj AbySalto aplikacije nastavljen je na postjći ASP.NET framework (Target framework net9.0), dodana je ClienApp Angular 19 aplikacija za frontend.
Za bazu podataka koristi se SQL, za ORM je EFCore, korišten je Code first pristup.
Za Identity i role management korišten je ASP.NET Identity (basic)
Aplikacija zbog demonstrativnih svrha koristi ProductSeed za dohvatanje podataka sa dummyjson API-ja kako bi postojao i primjer za dohvatanje proizvoda iz baze
podataka koristeći EFCore.
Potrebno je izvršiti migracije koje se nalaze i Infrastructue projektu (ConnectionString prilagoditi po potrebi).
Instalacija potrebnih paketa i biblioteka.
Nakon ovog aplikacija je spremna za rad.

(Potrudio sam se da zaokružim neku priču oko tehničkog zadatka, dio za dummyjson, opet da se koristi i lokalna baza i da se ponešto prikaže i na frontendu).
