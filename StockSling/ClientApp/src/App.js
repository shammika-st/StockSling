import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { MeetEva } from './components/MeetEva';
import { Admin } from './components/Admin';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/meeteva' component={MeetEva} />
        <Route path='/fetchdata' component={FetchData} />
        <Route path='/admin' component={Admin} />
      </Layout>
    );
  }
}
