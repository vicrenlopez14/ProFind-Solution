import React from 'react';
import {Stack, Text, Link, FontWeights, IStackTokens, IStackStyles, ITextStyles} from '@fluentui/react';
import logo from './logo.svg';
import './App.css';
import { Home } from './pages/home/Home';

const boldStyle: Partial<ITextStyles> = {root: {fontWeight: FontWeights.semibold}};
const stackTokens: IStackTokens = {childrenGap: 15};
const stackStyles: Partial<IStackStyles> = {
    root: {
        width: '960px',
        margin: '0 auto',
        textAlign: 'center',
        color: '#605e5c',
    },
};

export const App: React.FunctionComponent = () => {
    return (
       <Home saludo={"Hola alessandor"} despedida={"Adios alessandro"} ></Home>
    );
};
