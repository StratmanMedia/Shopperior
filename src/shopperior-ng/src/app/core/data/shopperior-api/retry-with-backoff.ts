import { Observable, of, throwError } from "rxjs";
import { mergeMap, retryWhen, delay } from "rxjs/operators";

const getErrorMessage = (maxRetry: number) => 
  `HTTP request retried ${maxRetry} times without success.`;
const DEFAULT_MAX_RETRIES = 100;
const DEFAULT_BACKOFF = 1000;

export function retryWithBackoff(delayMs: number, maxRetry = DEFAULT_MAX_RETRIES, backoffMs = DEFAULT_BACKOFF) {
  let retries = maxRetry;
  return (src: Observable<any>) => 
    src.pipe(
      retryWhen((errors: Observable<any>) => errors.pipe(
        mergeMap(error => {
          if (retries-- > 0) {
            const backoffTime = delayMs + (maxRetry - retries) * backoffMs;
            return of(error).pipe(delay(backoffTime));
          }
          return throwError(getErrorMessage(maxRetry));
        })
      ))
    );
}