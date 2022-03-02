export interface ListItemDto {
  guid: string;
  shoppingListGuid: string;
  categoryGuid: string;
  name: string;
  brand: string;
  comment: string;
  quantity: number;
  measurement: string;
  unitPrice: number;
  totalPrice: number;
  isInCart: boolean;
  hasPurchased: boolean;
}