import React, { Component } from 'react';
import { Route } from 'react-router';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Home } from './components/Home';
import { Layout } from './components/Layout';
import { FinanceList } from './components/FinanceList';

export default class App extends Component {
    displayName = App.name

      render() {
            return (
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route path='/counter' component={Counter} />
                    <Route path='/fetchdata' component={FetchData} />
                    <Route path='/test' component={FinanceList} />
                </Layout>
            );
      }
}
