import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

export function convertDateToNgDate(date: string): NgbDate {
  const dateSplit = date.split('-');
  return new NgbDate(
    Number.parseInt(dateSplit[0]),
    Number.parseInt(dateSplit[1]),
    Number.parseInt(dateSplit[2])
  );
}
