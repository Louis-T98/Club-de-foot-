export interface Entrainement {
  idEntrainement: number;
  idEquipe: number | null;
  nomEquipe: string | null;
  dateEntrainement: string | null;
  dureeMinutes: number | null;
  theme: string | null;
}