export interface Joueur {
  idJoueur: number;
  idEquipe: number | null;
  idPoste: number | null;
  nom: string;
  prenom: string;
  dateNaissance: string | null;
  nationalite: string | null;
  numeroMaillot: number | null;
}