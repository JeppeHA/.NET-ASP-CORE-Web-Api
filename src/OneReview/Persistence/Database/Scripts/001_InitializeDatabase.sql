CREATE TABLE IF NOT EXISTS players (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    age SMALLINT NOT NULL,
    gender TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS courses(
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    numberOfHoles INT NOT NULL,
    difficulty VARCHAR NOT NULL
);

CREATE TABLE IF NOT EXISTS holes(
    holeNumber INT NOT NULL,
    courseId UUID NOT NULL,
    par INT NOT NULL,
    PRIMARY KEY (holeNumber, courseId),
    FOREIGN KEY (courseId) REFERENCES courses(id)
);

CREATE TABLE IF NOT EXISTS rounds(
    id UUID PRIMARY KEY,
    playerId UUID NOT NULL,
    courseID UUID NOT NULL,
    roundDate TIMESTAMP NOT NULL,
    FOREIGN KEY (playerId) REFERENCES players(id),
    FOREIGN KEY (courseId) REFERENCES courses(id)
);

CREATE TABLE IF NOT EXISTS scores(
    roundId UUID NOT NULL,
    holeNumber INT NOT NULL,
    courseId UUID NOT NULL,
    strokes int NOT NULL,
    
    PRIMARY KEY (roundId, holeNumber,courseId),
    FOREIGN KEY (roundId) REFERENCES rounds(id),
    FOREIGN KEY (holeNumber, courseId) REFERENCES holes(holeNumber, courseId)
);
  