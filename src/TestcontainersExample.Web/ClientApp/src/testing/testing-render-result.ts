import { InjectFlags, InjectionToken, ProviderToken, Type } from '@angular/core';
import { RenderResult } from '@testing-library/angular';
import { InterceptAndFlush } from './intercept-and-flush';

export interface TestingComponentRenderResult<ComponentType> extends RenderResult<ComponentType, ComponentType> {
    getService: <T>(token: Type<T> | InjectionToken<T> | ProviderToken<T>, notFoundValue?: T, flags?: InjectFlags) => T;
    getComponentRef: () => ComponentType;
    flush: InterceptAndFlush<ComponentType>;
}

export interface TestingServiceRenderResult<ComponentType> {
    service: ComponentType;
    getService: <T>(token: Type<T> | InjectionToken<T> | ProviderToken<T>, notFoundValue?: T, flags?: InjectFlags) => T;
    flush: InterceptAndFlush<ComponentType>;
}
