DROP TABLE IF EXISTS
    Stripboek,
    Druk,
    Serie,
    Werkt_aan,
    Wordt_opgeslagen_in_collectie_van,
    Creator,
    Genre,
    Uitgever,
    Gebruiker
;

CREATE TABLE Gebruiker
(
    gebruikerId    int          NOT NULL AUTO_INCREMENT,
    gebruikersnaam nvarchar(20) NOT NULL,
    geboortedatum  date,
    email          nvarchar(50) NOT NULL,
    wachtwoord     nvarchar(50) NOT NULL,
    isAdmin        bool,
    PRIMARY KEY (gebruikerId)
);

CREATE TABLE Uitgever
(
    uitgeverId    int AUTO_INCREMENT,
    uitgever_naam nvarchar(40) NOT NULL,
    PRIMARY KEY (uitgeverId)
);

CREATE TABLE Creator
(
    creatorId    int          NOT NULL AUTO_INCREMENT,
    creator_naam nvarchar(40) NOT NULL,
    PRIMARY KEY (creatorId)
);

CREATE TABLE Genre
(
    genreId int AUTO_INCREMENT,
    genre   nvarchar(40),
    PRIMARY KEY (genreId)
);


CREATE TABLE Serie
(
    serieId            int AUTO_INCREMENT NOT NULL,
    serieTitel         nvarchar(30)       NOT NULL,
    land_van_oorsprong nvarchar(30)       NOT NULL,
    eerste_publicatie  date               NOT NULL,
    lopend             bool               NOT NULL,
    PRIMARY KEY (serieId)
);

CREATE TABLE Stripboek
(
    stripboekId    int          NOT NULL AUTO_INCREMENT,
    stripboektitel nvarchar(40) NOT NULL,
    aantal_paginas int,
    volgnummer     int          NOT NULL,
    genre_id       int,
    serie_id       int,
    imageUrl       nvarchar(5000),
    PRIMARY KEY (stripboekId),
    FOREIGN KEY (genre_id) REFERENCES Genre (GenreId),
    FOREIGN KEY (serie_id) REFERENCES Serie (serieId)
);

CREATE TABLE Druk
(
    drukId       int          NOT NULL AUTO_INCREMENT,
    stripboek_id int,
    druknummer   nvarchar(30) NOT NULL,
    druk_datum   date         NOT NULL,
    uitvoering   nvarchar(30),
    oplage       int,
    waarde       double,
    isbn         nvarchar(13),
    uitgever_id  int,
    PRIMARY KEY (drukId),
    FOREIGN KEY (stripboek_id) REFERENCES Stripboek (stripboekId) ON DELETE CASCADE,
    FOREIGN KEY (uitgever_id) REFERENCES Uitgever (uitgeverId)
);

CREATE TABLE Wordt_opgeslagen_in_collectie_van
(
    druk_id         int,
    gebruiker_id    int,
    staat           nvarchar(100),
    aankoop_waarde  double,
    aankoop_locatie nvarchar(40),
    beoordeling     tinyint,
    opslag_locatie  nvarchar(40),
    CONSTRAINT PK_in_collectie PRIMARY KEY (druk_id, gebruiker_id),
    FOREIGN KEY (druk_id) REFERENCES Druk (drukId) ON DELETE CASCADE,
    FOREIGN KEY (gebruiker_id) REFERENCES Gebruiker (gebruikerId) ON DELETE CASCADE
);

CREATE TABLE Werkt_aan
(
    creator_id int,
    druk_id    int,
    rol        nvarchar(40) NOT NULL,
    CONSTRAINT PK_Werkt_aan PRIMARY KEY (creator_id, druk_id, rol),
    FOREIGN KEY (druk_id) REFERENCES Druk (drukId) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES Creator (creatorId) ON DELETE CASCADE
);

INSERT INTO serie(serieTitel, land_van_oorsprong, eerste_publicatie, lopend)
VALUES ('Sneeuwwitje', 'USA', '1901-01-12', true),
       ('Suske en Wiske', 'Nederland', '1901-01-12', true),
       ('Batman', 'USA', '1901-01-12', false),
       ('Wildstorm', 'USA', '1901-01-12', true);

INSERT INTO Genre(genre)
VALUES ('Avondtuur'),
       ('Horror'),
       ('Actie'),
       ('Horror'),
       ('Drama');

INSERT INTO Uitgever(uitgever_naam)
VALUES ('Marvel'),
       ('DC'),
       ('Hanze Hogeschool'),
       ('NHL Stenden');

INSERT INTO Creator(creator_naam)
VALUES ('John Doe'),
       ('Allard de Groot'),
       ('Ahmad Hajan'),
       ('Sven Imholz'),
       ('Stan Lee');

INSERT INTO Stripboek (stripboektitel, aantal_paginas, volgnummer, serie_id, genre_id, imageUrl)
VALUES ('Sneeuwwitje en de zeven dwergen', 56, 1, 1, 1, 'jetbrains://rd/navigate/reference?project=WebDevProject&path=~/Documents/School/HBO-ICT/Module 3/WebDev_Project/Stripboek covers/PNG-afbeelding 1_compressed.png'),
       ('Sneeuwwitje en de zes dwergen', 86, 2, 1, 1, 'jetbrains://rd/navigate/reference?project=WebDevProject&path=~/Documents/School/HBO-ICT/Module 3/WebDev_Project/Stripboek covers/PNG-afbeelding 3_compressed.png'),
       ('Sneeuwwitje en de vijf dwergen', 65, 3, 1, 1, 'jetbrains://rd/navigate/reference?project=WebDevProject&path=~/Documents/School/HBO-ICT/Module 3/WebDev_Project/Stripboek covers/PNG-afbeelding 5_compressed.png'),
       ('Sneeuwwitje en waar zijn de dwergen?', 156, 4, 1, 1, 'jetbrains://rd/navigate/reference?project=WebDevProject&path=~/Documents/School/HBO-ICT/Module 3/WebDev_Project/Stripboek covers/PNG-afbeelding 7_compressed.png'),
       ('A man or a Bat?', 45, 1, 2, 2, 'jetbrains://rd/navigate/reference?project=WebDevProject&path=~/Documents/School/HBO-ICT/Module 3/WebDev_Project/Stripboek covers/PNG-afbeelding 9_compressed.png');

INSERT INTO Druk(druknummer, druk_datum, uitvoering, oplage, waarde, isbn, stripboek_id, uitgever_id)
VALUES (1, '1993-08-12', 'Nederlands', 3500, 12.89, '9783161484100', 1, 1),
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
VALUES ('svenimholz', '1901-01-12', 'sven@gmail.com', '1234'),
       ('Ahmad', '1901-01-12', 'ahmad@gmail.com', '1234'),
       ('allard', '1901-01-12', 'allard@gmail.com', '1234'),
       ('gebruiker1', '1901-01-12', 'gebruiker1@gmail.com', '1234'),
       ('gebruiker2', '1901-01-12', 'gebruiker2@gmail.com', '1234');


INSERT INTO wordt_opgeslagen_in_collectie_van (staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie,
                                               druk_id, gebruiker_id)
VALUES ('staat 1', 20, 'Marvel shop', 3, 'opslag locatie', 1, 1),
       ('staat 2', 25, 'Marvel shop', 4, 'opslag locatie', 2, 1),
       ('staat 3', 22, 'Marvel shop', 1, 'opslag locatie', 3, 1),
       ('staat 4', 22, 'Marvel shop', 1, 'opslag locatie', 4, 1);