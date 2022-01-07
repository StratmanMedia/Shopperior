export interface ApiResponseModel<T> {
  messages: string[];
  isSuccess: boolean;
  data: T;
}