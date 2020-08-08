CREATE TABLE Trimestre(
	IdTrimestre INTEGER PRIMARY KEY,
	Meses TEXT NOT NULL
);
 
CREATE TABLE Oferta(
	IdOferta INTEGER PRIMARY KEY,
	IdTrimestre INTEGER NOT NULL,
	Año INTEGER NOT NULL,
	FOREIGN KEY (IdTrimestre) REFERENCES Trimestre(IdTrimestre)
);


CREATE TABLE Seccion(
	IdSeccion INTEGER PRIMARY KEY,
	IdOferta INTEGER NOT NULL,
	tipo TEXT,
	idSec  TEXT,
	aula  TEXT,
	profesor  TEXT,
	lun  TEXT,
	mar  TEXT,
	mier  TEXT,
	jue  TEXT,
	vie  TEXT,
	sab  TEXT,
	asignatura  TEXT,
	area  TEXT,
	FOREIGN KEY (IdOferta) REFERENCES OFERTA(IdOferta)
);


INSERT INTO Trimestre VALUES
	(1,'FEBRERO - ABRIL'),
	(2,'MAYO - JULIO'),
	(3,'AGOSTO - OCTUBRE'),
	(4,'NOVIEMBRE - ENERO');