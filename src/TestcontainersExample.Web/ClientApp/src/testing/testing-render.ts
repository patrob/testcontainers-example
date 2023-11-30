import { HttpTestingController } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { render } from '@testing-library/angular';
import { InterceptAndFlush } from './intercept-and-flush';
import { getTestingDeclarations } from './testing-declarations';
import { getTestingImports } from './testing-imports';
import { getTestingProviders } from './testing-providers';
import { TestingComponentRenderResult } from './testing-render-result';

interface TestRenderOptions<T> {
    imports?: unknown[];
    providers?: unknown[];
    declarations?: unknown[];
    componentProperties?: Partial<T>;
}

export async function renderRootComponent<T>(
    component: Type<T>,
    { imports = [], providers = [], componentProperties = {}, declarations = [] }: TestRenderOptions<T> = {}
): Promise<TestingComponentRenderResult<T>> {
    const result = await render(component, {
        excludeComponentDeclaration: false,
        declarations: [...getTestingDeclarations(), ...declarations],
        imports: [...getTestingImports(), ...imports],
        providers: [...getTestingProviders(), ...providers],
        componentProperties,
    });
    return {
        ...result,
        getService: TestBed.inject,
        getComponentRef: () => result.fixture.componentRef.instance,
        flush: new InterceptAndFlush(TestBed.inject(HttpTestingController), result),
    };
}
