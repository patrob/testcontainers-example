import { HttpTestingController } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { InterceptAndFlush } from './intercept-and-flush';
import { getTestingImports } from './testing-imports';
import { getTestingProviders } from './testing-providers';
import { TestingServiceRenderResult } from './testing-render-result';

interface TestServiceOptions {
    imports?: unknown[];
    providers?: unknown[];
    declarations?: unknown[];
}

export async function createTestService<T>(
    service: Type<T>,
    { imports = [], providers = [], declarations = [] }: TestServiceOptions = {}
): Promise<TestingServiceRenderResult<T>> {
    TestBed.configureTestingModule({
        declarations: [...declarations],
        imports: [...getTestingImports(), ...imports],
        providers: [...getTestingProviders(), ...providers, service],
    });
    return {
        service: TestBed.inject(service),
        getService: TestBed.inject,
        flush: new InterceptAndFlush(TestBed.inject(HttpTestingController)),
    };
}
