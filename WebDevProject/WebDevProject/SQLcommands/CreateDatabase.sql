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
    gebruikerId   int          NOT NULL AUTO_INCREMENT,
    gebruikersnaam nvarchar(20) NOT NULL,
    geboortedatum  date,
    email          nvarchar(50) NOT NULL,
    wachtwoord     nvarchar(50) NOT NULL,
    PRIMARY KEY (gebruikerId)
);

CREATE TABLE Uitgever
(
    uitgeverId   int AUTO_INCREMENT,
    uitgever_naam nvarchar(40) NOT NULL,
    PRIMARY KEY (uitgeverId)
);

CREATE TABLE Creator
(
    creatorId   int          NOT NULL AUTO_INCREMENT,
    creator_naam nvarchar(40) NOT NULL,
    PRIMARY KEY (creatorId)
);

CREATE TABLE Genre
(
    genreId int AUTO_INCREMENT,
    genre    nvarchar(40),
    PRIMARY KEY (genreId)
);


CREATE TABLE Serie
(
    serieId           int AUTO_INCREMENT NOT NULL,
    serieTitel         nvarchar(30)       NOT NULL,
    land_van_oorsprong nvarchar(30)       NOT NULL,
    eerste_publicatie  date               NOT NULL,
    lopend             bool               NOT NULL,
    PRIMARY KEY (serieId)
);

CREATE TABLE Stripboek
(
    stripboekId   int          NOT NULL AUTO_INCREMENT,
    stripboektitel nvarchar(40) NOT NULL,
    aantal_paginas int,
    volgnummer     int          NOT NULL,
    genre_id       int,
    serie_id       int,
    PRIMARY KEY (stripboekId),
    FOREIGN KEY (genre_id) REFERENCES Genre (GenreId),
    FOREIGN KEY (serie_id) REFERENCES Serie (serieId)
);

CREATE TABLE Druk
(
    drukId      int          NOT NULL AUTO_INCREMENT,
    stripboek_id int,
    druknummer   nvarchar(30) NOT NULL,
    druk_datum   date         NOT NULL,
    uitvoering   nvarchar(30),
    oplage       int,
    waarde       double,
    isbn         nvarchar(13),
    uitgever_id  int,
    PRIMARY KEY (drukId),
    FOREIGN KEY (stripboek_id) REFERENCES Stripboek (stripboekId),
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
    FOREIGN KEY (druk_id) REFERENCES Druk (drukId),
    FOREIGN KEY (gebruiker_id) REFERENCES Gebruiker (gebruikerId)
);

CREATE TABLE Werkt_aan
(
    creator_id int,
    druk_id    int,
    rol        nvarchar(40) NOT NULL,
    CONSTRAINT PK_Werkt_aan PRIMARY KEY (creator_id, druk_id, rol),
    FOREIGN KEY (druk_id) REFERENCES Druk (drukId),
    FOREIGN KEY (creator_id) REFERENCES Creator (creatorId)
);
