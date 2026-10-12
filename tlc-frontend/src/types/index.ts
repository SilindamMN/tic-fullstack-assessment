export enum RedemptionStatus {
  Pending = 'Pending',
  Completed = 'Completed',
  Failed = 'Failed'
}

export interface Redemption {
  message?: string;
  redemptionId: number;
  participantName: string;
  countryCode: string;
  rewardName: string;
  pointsCost: number;
  status: RedemptionStatus;
  requestedAt: string;
}

export interface Participant {
  participantId: number;
  fullName: string;
  countryCode: string;
  pointsBalance: number;
  isActive: boolean;
}

export interface Reward {
  rewardId: number;
  name: string;
  pointsCost: number;
  isActive: boolean;
}

export interface CreateRedemptionRequest {
  participantId: number;
  rewardId: number;
}