-- =========================
-- BASE DE DONNÉES : CLUB DE FOOT
-- =========================
CREATE DATABASE IF NOT EXISTS club_foot3;
USE club_foot3;

-- =========================
-- TABLES
-- =========================

CREATE TABLE club (
    id_club INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    ville VARCHAR(100),
    stade VARCHAR(100),
    date_fondation DATE
);

CREATE TABLE equipe (
    id_equipe INT AUTO_INCREMENT PRIMARY KEY,
    id_club INT NOT NULL,
    nom VARCHAR(100),
    categorie VARCHAR(50),
    FOREIGN KEY (id_club) REFERENCES club(id_club)
);

CREATE TABLE poste (
    id_poste INT AUTO_INCREMENT PRIMARY KEY,
    libelle VARCHAR(50) NOT NULL
);

CREATE TABLE joueur (
    id_joueur INT AUTO_INCREMENT PRIMARY KEY,
    id_equipe INT,
    id_poste INT,
    nom VARCHAR(100),
    prenom VARCHAR(100),
    date_naissance DATE,
    nationalite VARCHAR(50),
    numero_maillot INT,
    FOREIGN KEY (id_equipe) REFERENCES equipe(id_equipe),
    FOREIGN KEY (id_poste) REFERENCES poste(id_poste)
);

CREATE TABLE staff (
    id_staff INT AUTO_INCREMENT PRIMARY KEY,
    id_equipe INT,
    nom VARCHAR(100),
    prenom VARCHAR(100),
    role VARCHAR(50),
    FOREIGN KEY (id_equipe) REFERENCES equipe(id_equipe)
);

CREATE TABLE contrat (
    id_contrat INT AUTO_INCREMENT PRIMARY KEY,
    id_joueur INT NOT NULL,
    date_debut DATE,
    date_fin DATE,
    salaire_mensuel DECIMAL(10,2),
    FOREIGN KEY (id_joueur) REFERENCES joueur(id_joueur)
);

CREATE TABLE match_foot (
    id_match INT AUTO_INCREMENT PRIMARY KEY,
    id_equipe INT,
    adversaire VARCHAR(100),
    date_match DATE,
    score_pour INT,
    score_contre INT,
    FOREIGN KEY (id_equipe) REFERENCES equipe(id_equipe)
);

CREATE TABLE entrainement (
    id_entrainement INT AUTO_INCREMENT PRIMARY KEY,
    id_equipe INT,
    date_entrainement DATE,
    duree_minutes INT,
    theme VARCHAR(100),
    FOREIGN KEY (id_equipe) REFERENCES equipe(id_equipe)
);

CREATE TABLE presence_entrainement (
    id_entrainement INT,
    id_joueur INT,
    present BOOLEAN,
    PRIMARY KEY (id_entrainement, id_joueur),
    FOREIGN KEY (id_entrainement) REFERENCES entrainement(id_entrainement),
    FOREIGN KEY (id_joueur) REFERENCES joueur(id_joueur)
);

CREATE TABLE statistique_match (
    id_match INT,
    id_joueur INT,
    buts INT DEFAULT 0,
    passes_decisives INT DEFAULT 0,
    cartons_jaunes INT DEFAULT 0,
    cartons_rouges INT DEFAULT 0,
    minutes_jouees INT,
    PRIMARY KEY (id_match, id_joueur),
    FOREIGN KEY (id_match) REFERENCES match_foot(id_match),
    FOREIGN KEY (id_joueur) REFERENCES joueur(id_joueur)
);

CREATE TABLE blessure (
    id_blessure INT AUTO_INCREMENT PRIMARY KEY,
    id_joueur INT NOT NULL,
    type_blessure VARCHAR(100),
    date_debut DATE,
    date_fin DATE,
    FOREIGN KEY (id_joueur) REFERENCES joueur(id_joueur)
);

-- =========================
-- INDEX
-- =========================

CREATE INDEX idx_joueur_equipe ON joueur(id_equipe);
CREATE INDEX idx_joueur_poste ON joueur(id_poste);
CREATE INDEX idx_match_date ON match_foot(date_match);
CREATE INDEX idx_contrat_joueur ON contrat(id_joueur);
CREATE INDEX idx_presence_entrainement ON presence_entrainement(id_entrainement, id_joueur);

-- =========================
-- TRIGGERS
-- =========================

DELIMITER $$

-- Vérifier la validité des dates de contrat
CREATE TRIGGER trg_check_contrat_date
BEFORE INSERT ON contrat
FOR EACH ROW
BEGIN
    IF NEW.date_fin <= NEW.date_debut THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Date de fin de contrat invalide';
    END IF;
END$$

-- Vérifier la cohérence des blessures
CREATE TRIGGER trg_check_blessure_date
BEFORE INSERT ON blessure
FOR EACH ROW
BEGIN
    IF NEW.date_fin IS NOT NULL AND NEW.date_fin < NEW.date_debut THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Date de fin de blessure invalide';
    END IF;
END$$

DELIMITER ;

-- =========================
-- VIEW (INNER JOIN)
-- =========================

CREATE VIEW vue_joueurs_equipes AS
SELECT
    j.id_joueur,
    j.nom,
    j.prenom,
    e.nom AS equipe,
    p.libelle AS poste
FROM joueur j
INNER JOIN equipe e ON j.id_equipe = e.id_equipe
INNER JOIN poste p ON j.id_poste = p.id_poste;

-- =========================
-- EXEMPLE D’UPDATE
-- =========================

-- Mettre à jour le numéro de maillot d’un joueur
UPDATE joueur
SET numero_maillot = 10
WHERE id_joueur = 1;

USE club_foot3;

-- =========================
-- CLUBS
-- =========================
INSERT INTO club (nom, ville, stade, date_fondation)
VALUES
('FC Lion', 'Bruxelles', 'Stade Royal', '1990-05-10'),
('Sporting Eagle', 'Anvers', 'Eagle Stadium', '1985-08-22');

-- =========================
-- EQUIPES
-- =========================
INSERT INTO equipe (id_club, nom, categorie)
VALUES
(1, 'FC Lion A', 'Senior'),
(1, 'FC Lion B', 'Junior'),
(2, 'Sporting Eagle A', 'Senior');

-- =========================
-- POSTES
-- =========================
INSERT INTO poste (libelle)
VALUES
('Gardien'),
('Défenseur'),
('Milieu'),
('Attaquant');

-- =========================
-- JOUEURS
-- =========================
INSERT INTO joueur (id_equipe, id_poste, nom, prenom, date_naissance, nationalite, numero_maillot)
VALUES
(1, 1, 'Dubois', 'Marc', '1995-01-15', 'Belgique', 1),
(1, 2, 'Lemoine', 'Pierre', '1994-03-22', 'Belgique', 4),
(1, 3, 'Moreau', 'Jean', '1996-07-30', 'France', 8),
(1, 4, 'Rousseau', 'Luc', '1995-11-12', 'France', 10),
(2, 1, 'Van Damme', 'Tom', '2004-02-18', 'Belgique', 1),
(2, 2, 'Janssens', 'Alex', '2005-05-05', 'Belgique', 3),
(3, 1, 'De Clercq', 'Bram', '1993-09-09', 'Belgique', 1),
(3, 4, 'Peeters', 'Sven', '1992-12-12', 'Belgique', 9);

-- =========================
-- STAFF
-- =========================
INSERT INTO staff (id_equipe, nom, prenom, role)
VALUES
(1, 'Martin', 'Paul', 'Entraîneur'),
(1, 'Fournier', 'Claire', 'Préparateur physique'),
(2, 'Dupont', 'Luc', 'Entraîneur'),
(3, 'Vandermeer', 'Lies', 'Entraîneur');

-- =========================
-- CONTRATS
-- =========================
INSERT INTO contrat (id_joueur, date_debut, date_fin, salaire_mensuel)
VALUES
(1, '2024-01-01', '2025-12-31', 3000),
(2, '2023-07-01', '2024-06-30', 2500),
(3, '2024-01-01', '2026-12-31', 2800),
(4, '2024-02-01', '2025-02-01', 3500),
(5, '2023-09-01', '2024-08-31', 1500),
(6, '2023-09-01', '2024-08-31', 1500),
(7, '2023-01-01', '2024-12-31', 3200),
(8, '2023-01-01', '2024-12-31', 3100);

-- =========================
-- MATCHS
-- =========================
INSERT INTO match_foot (id_equipe, adversaire, date_match, score_pour, score_contre)
VALUES
(1, 'Sporting Eagle A', '2025-03-15', 2, 1),
(1, 'Sporting Eagle A', '2025-04-10', 1, 3),
(2, 'FC Lion A', '2025-03-20', 0, 4),
(3, 'FC Lion A', '2025-05-01', 3, 2);

-- =========================
-- ENTRAINEMENTS
-- =========================
INSERT INTO entrainement (id_equipe, date_entrainement, duree_minutes, theme)
VALUES
(1, '2025-12-01', 90, 'Tactique offensive'),
(1, '2025-12-03', 90, 'Tir et finition'),
(2, '2025-12-02', 60, 'Condition physique'),
(3, '2025-12-01', 90, 'Stratégie match');

-- =========================
-- PRESENCE ENTRAINEMENTS
-- =========================
INSERT INTO presence_entrainement (id_entrainement, id_joueur, present)
VALUES
(1, 1, TRUE),
(1, 2, TRUE),
(1, 3, TRUE),
(1, 4, FALSE),
(2, 1, TRUE),
(2, 2, TRUE),
(2, 3, TRUE),
(2, 4, TRUE),
(3, 5, TRUE),
(3, 6, TRUE),
(4, 7, TRUE),
(4, 8, TRUE);

-- =========================
-- STATISTIQUES MATCH
-- =========================
INSERT INTO statistique_match (id_match, id_joueur, buts, passes_decisives, cartons_jaunes, cartons_rouges, minutes_jouees)
VALUES
(1, 1, 0, 1, 0, 0, 90),
(1, 2, 1, 0, 0, 0, 90),
(1, 3, 1, 1, 1, 0, 90),
(1, 4, 0, 0, 0, 0, 0),
(2, 1, 0, 0, 1, 0, 90),
(2, 2, 0, 0, 0, 0, 90),
(3, 5, 0, 0, 0, 0, 60),
(4, 7, 2, 0, 0, 0, 90),
(4, 8, 1, 1, 0, 0, 90);

-- =========================
-- BLESSURES
-- =========================
INSERT INTO blessure (id_joueur, type_blessure, date_debut, date_fin)
VALUES
(2, 'Entorse cheville', '2025-01-10', '2025-01-20'),
(3, 'Contracture', '2025-02-05', '2025-02-12'),
(6, 'Fracture main', '2025-03-01', '2025-03-30');
