import { LogLevel } from "./log-level";
import { LoggingOptions } from "./logging-options";

export class LoggingService {
  private _minLogLevel = LogLevel.ERROR;

  constructor(private _options: LoggingOptions) {
    this.resolveOptions();
  }

  trace(message: string) {
    if (this._minLogLevel <= LogLevel.TRACE) {
      console.log(`${this._options.callerName}:TRACE: ${message}`);
    }
  }

  debug(message: string) {
    if (this._minLogLevel <= LogLevel.DEBUG) {
      console.log(`${this._options.callerName}:DEBUG: ${message}`);
    }
  }

  info(message: string) {
    if (this._minLogLevel <= LogLevel.INFO) {
      console.log(`${this._options.callerName}:INFO: ${message}`);
    }
  }

  warn(message: string) {
    if (this._minLogLevel <= LogLevel.WARN) {
      console.warn(`${this._options.callerName}:WARN: ${message}`);
    }
  }

  error(message: string) {
    if (this._minLogLevel <= LogLevel.ERROR) {
      console.error(`${this._options.callerName}:ERROR: ${message}`);
    }
  }

  private resolveOptions(): void {
    switch(this._options.minimumLogLevel.toUpperCase()) {
      case 'TRACE': { this._minLogLevel = LogLevel.TRACE; break; }
      case 'DEBUG': { this._minLogLevel = LogLevel.DEBUG; break; }
      case 'INFO': { this._minLogLevel = LogLevel.INFO; break; }
      case 'WARN': { this._minLogLevel = LogLevel.WARN; break; }
      default: { this._minLogLevel = LogLevel.ERROR; break; }
    }
  }
}
