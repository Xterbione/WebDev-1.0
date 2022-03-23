INSERT INTO serie(serieTitel, land_van_oorsprong, eerste_publicatie, lopend)
VALUES  ('Batman', 'USA', '1901-01-12', true),
        ('Suske en Wiske', 'Nederland', '1901-01-12', false),
        ('Kuifje', 'België', '1901-01-12', true),
        ('Wildstorm', 'USA', '1901-01-12', true);

INSERT INTO Genre(genre)
VALUES  ('Avondtuur'),
        ('Horror'),
        ('Actie'),
        ('Horror'),
        ('Drama');

INSERT INTO Uitgever(uitgever_naam)
VALUES  ('Marvel'),
        ('DC'),
        ('Hanze Hogeschool'),
        ('NHL Stenden');

INSERT INTO Creator(creator_naam)
VALUES ('John Doe'),
       ('Allard de Groot'),
       ('Ahmad Hajan'),
       ('Sven Imholz'),
       ('Stan Lee');

INSERT INTO Stripboek (stripboektitel, aantal_paginas, volgnummer, serie_id, genre_id)
VALUES  ('Sneeuwwitje en de zeven dwergen', 56, 1, 1, 1),
        ('Sneeuwwitje en de zes dwergen', 86, 2, 1, 1),
        ('Sneeuwwitje en de vijf dwergen', 65, 3, 1, 1),
        ('Sneeuwwitje en waar zijn de dwergen?', 156, 4, 1, 1),
        ('A man or a Bat?', 45, 1, 2, 2);

INSERT INTO Druk(druknummer, druk_datum, uitvoering, oplage, waarde, isbn, stripboek_id, uitgever_id) 
VALUES  (1, '1993-08-12', 'Nederlands', 3500, 12.89, '9783161484100', 1, 1),
        (2, '1995-08-12', 'Nederlands', 20000, 8.09, '9783161484100', 1, 1),
        (3, '1997-08-12', 'Nederlands', 35000, 7.33, '9783161484100', 1, 2),
        (4, '1999-08-12', 'Nederlands', 12345, 3.33, '9783161484100', 1, 3),
        (5, '2000-08-12', 'Nederlands', 54321, 1.11, '9783161484100', 1, 4);

INSERT INTO Werkt_aan(creator_id, druk_id, rol)
VALUES (1, 1, 'Schrijver'),
       (1, 1, 'Tekenaar'),
       (2, 2, 'Tekenaar'),
       (2, 2, 'Schrijver'),
       (3, 3, 'Tekenaar'),
       (3, 3, 'Schrijver'),
       (4, 4, 'Tekenaar'),
       (4, 4, 'Schrijver'),
       (5, 5, 'Tekenaar'),
       (5, 5, 'Schrijver');

INSERT INTO gebruiker(gebruikersnaam, geboortedatum, email, wachtwoord)
VALUES  ('svenimholz', '1901-01-12', 'sven@gmail.com', '1234'),
        ('Ahmad', '1901-01-12', 'ahmad@gmail.com', '1234'),
        ('allard', '1901-01-12', 'allard@gmail.com', '1234'),
        ('gebruiker1', '1901-01-12', 'gebruiker1@gmail.com', '1234'),
        ('gebruiker2', '1901-01-12', 'gebruiker2@gmail.com', '1234');


INSERT INTO wordt_opgeslagen_in_collectie_van (staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie, druk_id, gebruiker_id)
VALUES  ('staat 1', 20, 'Marvel shop', 3,'opslag locatie', 1, 1),
        ('staat 2', 25, 'Marvel shop', 4,'opslag locatie', 2, 1),
        ('staat 3', 22, 'Marvel shop', 1,'opslag locatie', 3, 1),
        ('staat 4', 22, 'Marvel shop', 1,'opslag locatie', 4, 1);