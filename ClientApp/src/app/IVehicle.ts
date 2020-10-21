export interface IKeyValuePair {
  id: number;
  name: string;
}

export interface IContact {
  name: string;
  phone: string;
  email: string;
}

export interface IVehicle {
  id: number;
  model: IKeyValuePair;
  make: IKeyValuePair;
  isRegistered: boolean;
  features: IKeyValuePair[];
  contact: IContact;
  lastUpdate: string;
}

export interface ISaveVehicle {
  id: number;
  modelId: number;
  makeId: number;
  isRegistered: boolean;
  features: number[];
  contact: IContact;
}
