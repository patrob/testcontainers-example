import { Config } from 'jest';

const config: Config = {
  transform: {
    '<rootDir>/tsconfig.spec.json': ['ts-jest', { isolatedModules: true }],
  },
  testPathIgnorePatterns: ['/node_modules/'],
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  coverageReporters: ['cobertura', 'lcov', 'text-summary'],
  coverageDirectory: 'coverage',
  coveragePathIgnorePatterns: ['<rootDir>/src/testing', '<rootDir>/node_modules'],
  coverageThreshold: {
    global: {
      branches: 90,
      functions: 90,
      lines: 90,
      statements: 90,
    },
  },
};

export default config;
