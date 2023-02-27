import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    age: number;
    photoUrl: string;
    created: Date;
    lastActive: Date;
    knownAs: string;
    gender: string;
    introduction: string;
    lookingFor: string;
    interests: string;
    city: string;
    country: string;
    photos: Photo[];
}
