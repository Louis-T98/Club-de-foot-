export interface Statistique {
  idMatch: number;
  idJoueur: number;
  nomJoueur: string | null;
  buts: number;
  passesDecisives: number;
  cartonsJaunes: number;
  cartonsRouges: number;
  minutesJouees: number | null;
}