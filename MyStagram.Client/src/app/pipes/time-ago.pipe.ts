import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeAgo'
})
export class TimeAgoPipe implements PipeTransform {

  transform(value: any): any {
    if (value) {
      const differenceInSeconds = Math.floor((+new Date() - +new Date(value)) / 1000);
      if (differenceInSeconds < 30) {
        return 'now';
      }

      const timeIntervals = {
        years: 31536000,
        months: 2592000,
        weeks: 604800,
        days: 86400,
        hours: 3600,
        minutes: 60,
        seconds: 1,
      };

      let counter;
      // tslint:disable-next-line: forin
      for (const i in timeIntervals) {
        counter = Math.floor(differenceInSeconds / timeIntervals[i]);
        if (counter > 0) {
          if (counter === 1 && i === 'days') {
            return `${counter} day ago`;
          } else {
            return `${counter} ${i} ago`;
          }
        }
      }
    }

    return value;
  }
}
