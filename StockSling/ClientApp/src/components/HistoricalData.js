import React, { Component } from 'react';

import { AgGridReact } from 'ag-grid-react';
import 'ag-grid/dist/styles/ag-grid.css';
import 'ag-grid/dist/styles/ag-theme-balham.css';

export class HistoricalData extends Component {
  displayName = HistoricalData.name

    constructor(props) {
        super(props);
        this.state = {
            columnDefs: [
                { headerName: "StockName", field: "stockName" },
                { headerName: "IV", field: "iv" },
                { headerName: "Credit", field: "isCreditType" },
                { headerName: "Value", field: "typeValue"}
            ],
            loading: true
        }

        this.renderHistoricalDataTable = this.renderHistoricalDataTable.bind(this);
    }

    componentDidMount() {
        fetch('api/Forecast/HistoricalData')
            .then(response => response.json())
            .then(rowData => this.setState({ rowData, loading: false }));
    }

  renderHistoricalDataTable() {
    return (
        <div
            className="ag-theme-balham" 
            style={{
                height: '500px',
                width: '800px'
            }}
        >
            <AgGridReact
                enableSorting={true}
                enableFilter={true}
                columnDefs={this.state.columnDefs}
                rowData={this.state.rowData}>
            </AgGridReact>
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderHistoricalDataTable();

    return (
      <div>
        <h1>Historical Trade Data</h1>
        {contents}
      </div>
    );
  }
}
