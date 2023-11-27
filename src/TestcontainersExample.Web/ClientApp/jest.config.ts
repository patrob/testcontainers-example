import type { Config } from 'jest';

const config: Config = {
  verbose: true,
  preset: 'jest-preset-angular',
  setupFiles: ['<rootDir>/setup-jest.ts'],
};
export default config;
